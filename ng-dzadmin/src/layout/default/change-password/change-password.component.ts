import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { CommonService } from 'services';
import { MessageService } from 'abp-ng2-module/dist/src/message/message.service';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';

//import * as _ from "lodash";

@Component({
    selector: 'change-password-modal',
    templateUrl: './change-password.component.html',
    providers: [CommonService]
})
export class ChangePasswordComponent implements OnInit {
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    modalVisible = false;
    isConfirmLoading = false;
    isDisablec = false;
    isOldPasswordValid = false;
    //roles: RoleDto[] = null;

    validateForm: FormGroup;
    //roles: RoleDto[] = null;
    roles: any = [];

    orgPassword: string;
    newPassword: string;
    checkPassword: string;

    constructor(
        private _commonService: CommonService,
        private message: MessageService,
        private notify: NotifyService,
        private fb: FormBuilder
    ) {
    }

    ngOnInit(): void {
        this.validateForm = this.fb.group({
            orgPassword: [null, [Validators.required]],
            newPassword: [null, [Validators.required]],
            checkPassword: [null, Validators.compose([Validators.required, this.confirmationValidator])]
        });

    }

    show(): void {
        this.reset();
        this.modalVisible = true;
        //对isDisablec做初始化
        this.isDisablec = false;
    }

    save(isSave = false): void {
        for (const i in this.validateForm.controls) {
            this.validateForm.controls[i].markAsDirty();
        }
        this.isConfirmLoading = true
        if (this.validateForm.valid) {
            this._commonService.checkOldPassword(this.orgPassword).subscribe((isValid: boolean) => {
                if (isValid === true) {
                    this._commonService.updatePassword(this.newPassword)
                        .finally(() => { this.isConfirmLoading = false; })
                        .subscribe((result) => {
                            if (result === true) {
                                this.notify.success('修改密码成功');
                                this.close();
                                this.modalSave.emit(null);
                            } else {
                                this.message.error('修改密码失败');
                                //this.close();
                                //this.modalSave.emit(null);
                            }
                        });
                } else {
                    this.isConfirmLoading = false;
                    // this.isOldPasswordValid=true;
                    this.message.error('原密码错误');
                }
            });
        }
    }

    close(): void {
        this.modalVisible = false;
        //this.modal.hide();
    }

    handleCancel = (e) => {
        this.modalVisible = false;
        this.isConfirmLoading = false;
        this.reset(e);
    }


    /**
     * 新密码确认验证
     */
    confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
        if (!control.value) {
            return { required: true };
        } else if (control.value !== this.validateForm.controls['newPassword'].value) {
            return { confirm: true, error: true };
        }
    }


    getFormControl(name: string) {
        return this.validateForm.controls[name];
    }

    reset(e?): void {
        if (e) {
            e.preventDefault();
        }
        this.validateForm.reset();
        for (const key in this.validateForm.controls) {
            this.validateForm.controls[key].markAsPristine();
        }
    }


}
