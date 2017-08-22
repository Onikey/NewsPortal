import { ToastrService } from './../../../common/toastr/toastr.service';
import { DeleteModalOptions } from './../../../common/templates/delete-modal.component';
import { NewsService } from './../../services/news.service';
import { INewsItem } from './../../model/news.model';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit {
  public newsItems: INewsItem[];
  public isDeleteNewsItem: boolean = false;
  public deleteModalOptions: DeleteModalOptions;
  public loading = false;

  constructor(
    private _route: ActivatedRoute,
    private _newsService: NewsService,
    private _toastrService: ToastrService
  ) { }

  ngOnInit() {
    this.newsItems = this._route.snapshot.data['newsItems'];
  }

  public delete(id: string) {
    this.isDeleteNewsItem = true;
    this.deleteModalOptions = {
      itemName: 'news item',
      onApprove: () => {
        this.loading = true;
        this._newsService.deleteNews(id)
          .subscribe(response => {
            _.remove(this.newsItems, {
              id: id
            });
            this._toastrService.warning('Item has been deleted!');
          }, () => {
            this._toastrService.error('Items are not getting deleted');
          }, () => {
            this.loading = false;
          });
      },
      onHidden: () => { this.isDeleteNewsItem = false; }
    }
  }
}
