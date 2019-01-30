import { Component, Output, EventEmitter, ViewChild } from '@angular/core';
import { CropperSettings, ImageCropperComponent } from 'ngx-img-cropper';

@Component({
    selector: 'cropper',
    templateUrl: 'cropper.component.html'
})
export class CropperComponent {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    data: any;
    cropperSettings: CropperSettings;
    @ViewChild('cropper', undefined)
    cropper: ImageCropperComponent;
    loading = false;
    isVisible = false;
    title: string;
    constructor(
    ) {
        this.cropperSettings = new CropperSettings();
        this.cropperSettings.width = 460;
        this.cropperSettings.height = 460;
        this.cropperSettings.canvasWidth = 470;
        this.cropperSettings.canvasHeight = 460;
        this.cropperSettings.croppedWidth = 460;
        this.cropperSettings.croppedHeight = 460;
        this.cropperSettings.noFileInput = true;
        this.cropperSettings.cropOnResize = true;
        this.data = {};
    }

    show() {
        this.isVisible = true;
        this.data = {};
    }

    ngOnDestroy() {
        console.log('onDestroy');
    }


    handleCancel = (e) => {
        this.isVisible = false;
        // let { } = this.cropperSettings;
        // this.data = {};
        this.clear();
    }

    clear() {
        this.cropper.reset();
    }

    upload(e: any) {
        this.modalSelect.emit(e);
        this.isVisible = false;
        this.clear();
    }

    beforeUpload = (file: File): boolean => {
        var image: any = new Image();
        var myReader: FileReader = new FileReader();
        var that = this;
        myReader.onloadend = function (loadEvent: any) {
            image.src = loadEvent.target.result;
            that.cropper.setImage(image);
        };
        myReader.readAsDataURL(file);
        return false;
    }
}
