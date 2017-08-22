import { NewsPortalPage } from './app.po';

describe('news-portal App', () => {
  let page: NewsPortalPage;

  beforeEach(() => {
    page = new NewsPortalPage();
  });

  it('should display welcome message', done => {
    page.navigateTo();
    page.getParagraphText()
      .then(msg => expect(msg).toEqual('Welcome to app!!'))
      .then(done, done.fail);
  });
});
