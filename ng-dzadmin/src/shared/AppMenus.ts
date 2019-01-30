import { Menu } from '@delon/theme';

// 全局的左侧导航菜单
export class AppMenus {
    // new
    static Menus: Menu[] = [
        {
            text: "主页",// 无本地化显示字符
            //i18n: "HomePage",// 本地化主键(ABP本地化)
            acl: "",// 权限
            icon: { type: "icon", value: "home" },// 图标
            link: "/app/home", // url 地址
            // hide: true,  // 强制隐藏
            // ...还有更多选项，请查看 Menu成员
        },
        {
            text: "微信配置",
            icon: { type: "icon", value: "wechat" },
            link: "/app/wechat/config",
            reuse: false
        },
        {
            text: "资讯管理",
            icon: { type: "icon", value: "laptop" },
            link: "/app/news/news",
            reuse: false
        },
        {
            text: "积分商城",
            icon: { type: "icon", value: "shopping" },
            link: "/app/mall",
            group: true,
            children: [
                {
                    text: "会员管理",
                    link: "/app/mall/member",
                    reuse: false
                },
                {
                    text: "VIP管理",
                    link: "/app/mall/vip",
                    reuse: false
                },
                {
                    text: "直营店",
                    link: "/app/mall/shop",
                    reuse: false
                },
                {
                    text: "商品管理",
                    link: "/app/mall/goods",
                    reuse: false
                },
                {
                    text: "订单管理",
                    link: "/app/mall/order",
                    reuse: false
                },
                {
                    text: "兑换统计",
                    link: "/app/mall/exchange",
                    reuse: false
                },
            ]
        },
        {
            text: "系统管理",
            icon: { type: "icon", value: "setting" },
            link: "/app/system",
            group: true,
            children: [
                {
                    text: "用户管理",
                    link: "/app/system/users",
                    reuse: false
                },
                {
                    text: "角色管理",
                    link: "/app/system/roles",
                    reuse: false
                },
            ]
        },
    ];
    // old
    // static Menus = [
    //     // 首页
    //     new MenuItem(
    //         'HomePage',
    //         '',
    //         'anticon anticon-home',
    //         '/app/home'
    //     ),
    //     // 租户
    //     new MenuItem(
    //         'Tenants',
    //         'Pages.Tenants',
    //         'anticon anticon-team',
    //         '/app/tenants',
    //     ),
    //     // 角色
    //     new MenuItem(
    //         'Roles',
    //         'Pages.Roles',
    //         'anticon anticon-safety',
    //         '/app/roles',
    //     ),
    //     // 用户
    //     new MenuItem(
    //         'Users',
    //         'Pages.Users',
    //         'anticon anticon-user',
    //         '/app/users',
    //     ),
    //     // 关于我们
    //     new MenuItem(
    //         'About',
    //         '',
    //         'anticon anticon-info-circle-o',
    //         '/app/about',
    //     ),
    // ];
}