import { INewsItem } from './../model/news.model';
import { Observable } from 'rxjs/Rx';
import { Http, RequestOptions, Response, Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class NewsService {
    private urlPath = 'http://localhost:44394/api/news'
    constructor(private http: Http) { }

    public getAllNews(): Observable<INewsItem[]> {
        return this.http.get(this.urlPath)
            .map(this.extractData)
            .catch(this.handleError);
    }

    public getNewsById(id: string): Observable<INewsItem> {
        const url = `${this.urlPath}/${id}`
        return this.http.get(url)
            .map(this.extractData)
            .catch(this.handleError);
    }

    public addNews(item: INewsItem): Observable<string> {
        const url = `${this.urlPath}/add`;
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this.http.post(url, JSON.stringify(item), options)
            .map(response => response)
            .catch(this.handleError);
    }

    public updateNews(item: INewsItem): Observable<string> {
        const url = `${this.urlPath}/update`;
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        return this.http.put(url, JSON.stringify(item), options)
            .map(response => response)
            .catch(this.handleError);
    }

    public deleteNews(id: string): Observable<string> {
        const url = `${this.urlPath}/delete/${id}`;

        return this.http.delete(url)
            .map(response => response)
            .catch(this.handleError);
    }

    private extractData(response: Response) {
        const body = response.json();
        return body || {};
    }

    private handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }

        return Observable.throw(errMsg);
    }
}
