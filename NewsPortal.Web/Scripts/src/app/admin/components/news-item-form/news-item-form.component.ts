import { DeleteModalOptions } from './../../../common/templates/delete-modal.component';
import { ToastrService } from './../../../common/toastr/toastr.service';
import { Location } from '@angular/common';
import { AuthorService } from './../../services/author.service';
import { NewsService } from './../../services/news.service';
import { ActivatedRoute } from '@angular/router';
import { IAuthor } from './../../model/author.model';
import { Observable } from 'rxjs/Rx';
import { INewsItem } from './../../model/news.model';
import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

declare var $: any;

@Component({
  selector: 'app-news-item-form',
  templateUrl: './news-item-form.component.html',
  styleUrls: ['./news-item-form.component.css']
})
export class NewsItemFormComponent implements OnInit {
  public authors: Observable<IAuthor[]>;
  public isNewItem: boolean = false;
  public newsItemForm: FormGroup;
  public loadingForm: boolean = false;
  public newsItem: INewsItem;

  public isDeleteNewsItem: boolean = false;
  public deleteModalOptions: DeleteModalOptions;

  constructor(
    private _route: ActivatedRoute,
    private _newsService: NewsService,
    private _authorService: AuthorService,
    private _location: Location,
    private _fb: FormBuilder,
    private _toastrService: ToastrService
  ) {
    this.newsItem = this._route.snapshot.data['newsItem'];

    if (this.newsItem === undefined) {
      this.newsItem = {
        title: '',
        id: '',
        content: '',
        author: '',
        authorId: ''
      };

      this.isNewItem = true;
    }
    this.newsItemForm = _fb.group({
      title: this.newsItem.title,
      content: this.newsItem.content,
      author: this.newsItem.authorId
    });
  }

  ngOnInit() {
    this.authors = this._authorService.getAll();

    $('#fakeSubmit')
      .popup({
        inline: true,
        hoverable: true,
        position: 'top left',
        delay: {
          show: 300,
          hide: 500
        }
      });
  }

  public submit(value: any) {
    this.loadingForm = true;

    this.newsItem.title = value.title;
    this.newsItem.content = value.content;
    this.newsItem.authorId = value.author;

    if (this.isNewItem) {
      this._newsService.addNews(this.newsItem)
        .subscribe(response => {
          this._toastrService.success('News created!');
          this.back();
        }, error => {
          this._toastrService.error(error._body);
          this.loadingForm = false;
        });
    } else {
      this._newsService.updateNews(this.newsItem)
        .subscribe(response => {
          this._toastrService.success('News modified!');
          this.back();
        }, error => {
          this._toastrService.error(error._body);
          this.loadingForm = false;
        });
    }
  }

  public delete(id: string) {
    this.isDeleteNewsItem = true;

    this.deleteModalOptions = {
      itemName: 'news item',
      onApprove: () => {
        this.loadingForm = true;
        this._newsService.deleteNews(id)
          .subscribe(response => {
            this._toastrService.warning('Item has been deleted!');
            this.back();
          }, () => {
            this._toastrService.error('Items are not getting deleted');
          }, () => {
            this.loadingForm = false;
          });
      },
      onHidden: () => { this.isDeleteNewsItem = false; }
    }
  }

  public back() {
    this._location.back();
  }

}
