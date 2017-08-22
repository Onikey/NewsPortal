import { HomeRoutingModule } from './home-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './components/home-page/home-page.component';
import { HomeNavBarComponent } from './components/common/home-nav-bar/home-nav-bar.component';
import { HomeComponent } from './home.component';

@NgModule({
    imports: [
        CommonModule,
        HomeRoutingModule
    ],
    declarations: [
        HomeNavBarComponent,
        HomePageComponent,
        HomeComponent
    ],
    exports: [
        HomeNavBarComponent,
        HomePageComponent,
        HomeComponent
    ]
})
export class HomeModule { }
