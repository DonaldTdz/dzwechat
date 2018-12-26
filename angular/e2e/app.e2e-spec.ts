import { dzwechatTemplatePage } from './app.po';

describe('dzwechat App', function() {
  let page: dzwechatTemplatePage;

  beforeEach(() => {
    page = new dzwechatTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
