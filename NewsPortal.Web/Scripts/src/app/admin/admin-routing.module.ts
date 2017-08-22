import { NewsItemFormComponent } from './components/news-item-form/news-item-form.component';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

import { NewsItemResolver } from './services/news-item-resolve.service';
import { NewsListResolver } from './services/news-list-resolve.service';

import { AdminComponent } from './admin.component';
import { HomeComponent } from './components/home/home.component';
import { SingleNewsItemComponent } from './components/single-news-item/single-news-item.component';
import { NewsListComponent } from './components/news-list/news-list.component';

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild([
        {
            path: '',
            component: AdminComponent,
            children: [
                {
                    path: '',
                    component: HomeComponent
                }, {
                    path: 'news-managment',
                    component: NewsListComponent,
                    resolve: {
                        newsItems: NewsListResolver
                    }
                }, {
                    path: 'news-item/edit/:id',
                    component: NewsItemFormComponent,
                    resolve: {
                        newsItem: NewsItemResolver
                    }
                }, {
                    path: 'news-item/add',
                    component: NewsItemFormComponent
                }
            ]
        }
    ])],
    exports: [RouterModule],
    providers: [],
    bootstrap: []
})
export class AdminRoutingModule { }
