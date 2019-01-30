import { Component, Output, EventEmitter, ViewChild } from '@angular/core';
import { CropperSettings, ImageCropperComponent } from 'ngx-img-cropper';

@Component({
    selector: 'banner-cropper',
    templateUrl: 'banner-cropper.component.html'
})
export class BannerCropperComponent {
    @Output() modalSelect: EventEmitter<any> = new EventEmitter<any>();
    bannerData: any;
    bannerSettings: CropperSettings;
    @ViewChild('banner', undefined)
    banner: ImageCropperComponent;
    loading = false;
    isVisible = false;
    title: string;
    constructor(
    ) {
        this.bannerSettings = new CropperSettings();
        this.bannerSettings.width = 500;
        this.bannerSettings.height = 230;
        this.bannerSettings.canvasWidth = 670;
        this.bannerSettings.canvasHeight = 400;
        this.bannerSettings.croppedWidth = 500;
        this.bannerSettings.croppedHeight = 230;
        this.bannerSettings.noFileInput = true;
        this.bannerSettings.cropOnResize = true;
        this.bannerData = {};
    }

    show() {
        this.isVisible = true;
        this.bannerData = {};
    }

    handleCancel = (e) => {
        this.isVisible = false;
        this.clear();
    }

    upload(e: any) {
        this.modalSelect.emit(e);
        this.isVisible = false;
        this.clear();
    }

    clear() {
        this.banner.reset();
    }

    beforeUpload = (file: File): boolean => {
        var image: any = new Image();
        var myReader: FileReader = new FileReader();
        var that = this;
        myReader.onloadend = function (loadEvent: any) {
            image.src = loadEvent.target.result;
            that.banner.setImage(image);
        };
        myReader.readAsDataURL(file);
        return false;
    }
}
