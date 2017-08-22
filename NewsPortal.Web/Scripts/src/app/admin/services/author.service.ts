import { IAuthor } from './../model/author.model';
import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthorService {
    private urlPath = 'http://localhost:44394/api/authors';

    constructor(private http: Http) { }

    public getAll(): Observable<IAuthor[]> {
        return this.http.get(`${this.urlPath}/short`)
            .map(this.extractData)
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
