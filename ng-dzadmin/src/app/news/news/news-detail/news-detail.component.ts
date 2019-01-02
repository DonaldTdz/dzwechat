import { Component, OnInit, Injector } from '@angular/core';
import { ModalComponentBase, AppComponentBase } from '@shared/component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { NewsService } from 'services';
import { News } from 'entities';
import { UploadFile } from 'ng-zorro-antd';
import { AppConsts } from '@shared/AppConsts';

@Component({
    moduleId: module.id,
    selector: 'news-detail',
    templateUrl: 'news-detail.component.html',
})
export class NewsDetailComponent extends AppComponentBase implements OnInit {
    title: string;
    id: string = '';
    newsType: number;
    news: News = new News;
    newsTypes = [
        { value: 1, text: '烟语课堂', selected: true },
        { value: 2, text: '新品快讯', selected: false },
        { value: 3, text: '产品大全', selected: false },
    ];
    linkTypes: [
        { value: 1, text: '外部链接', selected: true }
    ];
    actionUrl = '';
    isDelete = false;
    isPush = false;
    successMsg = '';
    constructor(injector: Injector, private actRoter: ActivatedRoute, private newsService: NewsService, private router: Router) {
        super(injector);
        this.id = this.actRoter.snapshot.params['id'];
        this.newsType = parseInt(this.actRoter.snapshot.params['newsType']);
        this.actionUrl = AppConsts.remoteServiceBaseUrl + '/WeChatFile/MarketingInfoPosts?fileName=news';
    }
    ngOnInit(): void {
        if (this.id) {
            this.title = '编辑资讯';
            this.getSingleNews();
            this.isDelete = true;
        } else {
            this.title = '新增资讯';
            this.news.pushStatus = 0;
            this.news.pushStatusName = '草稿';
            this.news.linkType = 1;
        }
        this.news.type = this.newsType;
    }

    //获取单个资讯
    getSingleNews() {
        this.newsService.getnewsById(this.id, this.newsType.toString()).subscribe((result) => {
            this.news = result;
            this.news.showCoverPhoto = AppConsts.remoteServiceBaseUrl + this.news.coverPhoto;
            this.isPush = result.pushStatus == 1 ? false : true;
        });
    }

    private getBase64(img: File, callback: (img: any) => void) {
        const reader = new FileReader();
        reader.addEventListener('load', () => callback(reader.result));
        reader.readAsDataURL(img);
    }

    //图片上传返回
    handleChange(info: { file: UploadFile }): void {

        if (info.file.status === 'error') {
            this.notify.error('上传图片异常，请重试');
        }
        if (info.file.status === 'done') {
            this.getBase64(info.file.originFileObj, (img: any) => {
                this.news.showCoverPhoto = img;
            });
            this.news.coverPhoto = info.file.response.result.imageName;
            this.notify.success('上传图片完成');
        }
    }

    //保存
    save(isPush) {
        this.successMsg = isPush ? '发布成功！' : '保存成功！'
        this.newsService.update(this.news)
            .finally(() => { this.saving = false; })
            .subscribe(() => {
                this.notify.info(this.l(this.successMsg));
            });
    }
    //删除
    delete(entiy: News): void {
        this.message.confirm(
            "删除资讯'" + entiy.title + "'?",
            '信息确认',
            (result: boolean) => {
                if (result) {
                    this.newsService.delete(entiy.id).subscribe(() => {
                        this.notify.info(this.l('已删除资讯' + entiy.title));
                        this.return();
                    });
                }
            });
    }

    //发布
    push() {
        this.news.pushStatus = 1;
        this.news.pushTime = this.dateFormatHH(new Date());
        this.save(true);
    }

    //返回
    return() {
        this.router.navigate(['app/news/news']);
    }
}
