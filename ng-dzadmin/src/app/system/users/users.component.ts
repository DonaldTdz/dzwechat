import { Component, OnInit, Injector } from '@angular/core';
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from '@shared/component-base/paged-listing-component-base';
import {
  PagedResultDtoOfUserDto,
  UserServiceProxy,
  UserDto,
} from '@shared/service-proxies/service-proxies';
import { CreateUserComponent } from '@app/system/users/create-user/create-user.component';
import { EditUserComponent } from '@app/system/users/edit-user/edit-user.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styles: [],
})
export class UsersComponent extends PagedListingComponentBase<UserDto> {
  constructor(injector: Injector, private _userService: UserServiceProxy) {
    super(injector);
  }

  protected fetchDataList(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function,
  ): void {
    this._userService
      .getAll(request.skipCount, request.maxResultCount)
      .finally(() => {
        finishedCallback();
      })
      .subscribe((result: PagedResultDtoOfUserDto) => {
        this.dataList = result.items;
        this.totalItems = result.totalCount;
      });
  }

  protected delete(entity: UserDto): void {
    this.message.confirm(
      "删除用户 '" + entity.name + "'?",
      '信息确认',
      (result: boolean) => {
        if (result) {
          this._userService.delete(entity.id).subscribe(() => {
            this.notify.info('已删除用户: ' + entity.name);
            this.refresh();
          });
        }
      },
    );
  }

  create(): void {
    this.modalHelper
      .open(CreateUserComponent, {}, 'md', {
        nzMask: true
      })
      .subscribe(isSave => {
        if (isSave) {
          this.refresh();
        }
      });
  }

  edit(item: UserDto): void {
    this.modalHelper
      .open(EditUserComponent, { id: item.id }, 'md', {
        nzMask: true
      })
      .subscribe(isSave => {
        if (isSave) {
          this.refresh();
        }
      });
  }
}
