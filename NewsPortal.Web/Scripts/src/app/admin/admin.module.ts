import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TinymceModule } from '../common/tinymce/tinymce.module';
import { ToastrModule } from './../common/toastr/toastr.module';
import { DeleteModalComponent } from './../common/templates/delete-modal.component';

import { AdminRoutingModule } from './admin-routing.module';

import { NewsListResolver } from './services/news-list-resolve.service';
import { NewsItemResolver } from './services/news-item-resolve.service';
import { NewsService } from './services/news.service';
import { AuthorService } from './services/author.service';

import { HomeComponent } from './components/home/home.component';
import { NewsListComponent } from './components/news-list/news-list.component';
import { AdminComponent } from './admin.component';
import { AdminNavBarComponent } from './components/admin-nav-bar/admin-nav-bar.component';
import { SingleNewsItemComponent } from './components/single-news-item/single-news-item.component';
import { NewsItemFormComponent } from './components/news-item-form/news-item-form.component';

declare let toastr: any;

@NgModule({
    imports: [
        CommonModule,
        HttpModule,
        AdminRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        ToastrModule,
        TinymceModule.withConfig({
            plugins: ['link paste table autoresize charmap searchreplace lists advlist textcolor colorpicker code'],
            toolbar: 'edit insert view format table tools'
        })
    ],
    declarations: [
        HomeComponent,
        NewsListComponent,
        AdminComponent,
        AdminNavBarComponent,
        SingleNewsItemComponent,
        NewsItemFormComponent,
        DeleteModalComponent
    ],
    exports: [
        HomeComponent,
        NewsListComponent,
        AdminComponent,
        AdminNavBarComponent,
        SingleNewsItemComponent
    ],
    providers: [
        NewsService,
        NewsListResolver,
        NewsItemResolver,
        AuthorService
    ]
})
export class AdminModule { }
