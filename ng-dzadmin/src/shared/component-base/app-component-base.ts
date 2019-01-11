import { Injector, ElementRef } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import { AppSessionService } from '@shared/session/app-session.service';
import { FeatureCheckerService } from '@abp/features/feature-checker.service';
import { NotifyService } from '@abp/notify/notify.service';
import { SettingService } from '@abp/settings/setting.service';
import { MessageService } from '@abp/message/message.service';
import { AbpMultiTenancyService } from '@abp/multi-tenancy/abp-multi-tenancy.service';
import { ModalHelper, ALAIN_I18N_TOKEN, TitleService } from '@delon/theme';
import { LocalizationService } from '@shared/i18n/localization.service';
import { PermissionService } from '@shared/auth/permission.service';

export abstract class AppComponentBase {
  localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;

  localization: LocalizationService;
  permission: PermissionService;
  feature: FeatureCheckerService;
  notify: NotifyService;
  setting: SettingService;
  message: MessageService;
  multiTenancy: AbpMultiTenancyService;
  appSession: AppSessionService;
  elementRef: ElementRef;
  modalHelper: ModalHelper;
  titleSrvice: TitleService;

  /**
   * 保存状态
   */
  saving = false;

  constructor(injector: Injector) {
    this.localization = injector.get<LocalizationService>(ALAIN_I18N_TOKEN);
    this.permission = injector.get(PermissionService);
    this.feature = injector.get(FeatureCheckerService);
    this.notify = injector.get(NotifyService);
    this.setting = injector.get(SettingService);
    this.message = injector.get(MessageService);
    this.multiTenancy = injector.get(AbpMultiTenancyService);
    this.appSession = injector.get(AppSessionService);
    this.elementRef = injector.get(ElementRef);
    this.modalHelper = injector.get(ModalHelper);
    this.titleSrvice = injector.get(TitleService);
  }

  l(key: string, ...args: any[]): string {
    let localizedText = this.localization.localization(key, this.localizationSourceName);

    if (!localizedText) {
      localizedText = key;
    }

    if (!args || !args.length) {
      return localizedText;
    }

    return this.localization.formatString(localizedText, args);
  }

  isGranted(permissionName: string): boolean {
    return this.permission.isGranted(permissionName);
  }

  dateFormatHH(date: any): string {
    if (date === null) {
      return null;
    }
    let d = new Date(date);
    let y = d.getFullYear().toString();
    let m = (d.getMonth() + 1).toString();
    let day = d.getDate().toString();
    let h = d.getHours();
    let ms = d.getMinutes();
    let hh = h > 10 ? h.toString() : '0' + h.toString();
    let mm = ms > 10 ? ms.toString() : '0' + ms.toString();
    return y + '-' + m + '-' + day + ' ' + hh + ':' + mm;
    // let dateStr:string = this.datePipe.transform(d,'yyyy-MM-dd');
    //return dateStr;
  }

  dateFormat(date: any): string {
    if (date === null) {
      return null;
    }
    let d = new Date(date);
    let y = d.getFullYear().toString();
    let m = (d.getMonth() + 1).toString();
    let day = d.getDate().toString();
    return y + '-' + m + '-' + day;
    //let dateStr:string = this.datePipe.transform(d,'yyyy-MM-dd');
    //return dateStr;
  }
}
