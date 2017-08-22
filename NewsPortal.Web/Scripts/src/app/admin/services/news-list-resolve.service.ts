import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { NewsService } from './news.service';
import { INewsItem } from './../model/news.model';

@Injectable()
export class NewsListResolver implements Resolve<INewsItem[]> {

    constructor(private _service: NewsService) { }
    resolve(
        route: ActivatedRouteSnapshot
    ): Observable<any> | Promise<any> | any {
        return this._service.getAllNews();
    }
}
