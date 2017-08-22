import { AuthorService } from './../../services/author.service';
import { Observable } from 'rxjs/Observable';
import { IAuthor } from './../../model/author.model';
import { NewsService } from './../../services/news.service';
import { ActivatedRoute } from '@angular/router';
import { INewsItem } from '../../model/news.model';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-single-news-item',
  templateUrl: './single-news-item.component.html',
  styleUrls: ['./single-news-item.component.css']
})
export class SingleNewsItemComponent implements OnInit {
  public authors: Observable<IAuthor[]>;
  public isNewItem: boolean = false;
  public model: INewsItem;
  public loadingForm: boolean = false;

  constructor(
    private _route: ActivatedRoute,
    private _newsService: NewsService,
    private _authorService: AuthorService,
    private _location: Location
  ) { }

  ngOnInit() {
    this.model = this._route.snapshot.data['newsItem'];

    if (this.model === undefined) {
      this.model = {
        id: null,
        title: null,
        content: null,
        author: null,
        authorId: null
      }

      this.isNewItem = true;
    }

    this.authors = this._authorService.getAll();
  }

  public submit() {
    this.loadingForm = true;
    if (this.isNewItem) {
      this._newsService.addNews(this.model)
        .subscribe(response => {
          // console.log(response);
          this.back();
        }, error => {
          console.log(error);
          this.loadingForm = false;
        });
    } else {
      this._newsService.updateNews(this.model)
        .subscribe(response => {
          // console.log(response);
          this.back();
        }, error => {
          console.log(error);
          this.loadingForm = false;
        });
    }
  }

  public delete(id: string) {
    this._newsService.deleteNews(id)
      .subscribe(response => {
        // console.log(response);
        this.back();
      })
  }

  public back() {
    this._location.back();
  }


}
