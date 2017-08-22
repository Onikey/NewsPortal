import { HomeComponent } from './home.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild([
        {
            path: '',
            component: HomeComponent,
            children: [
                {
                    path: '', component: HomePageComponent
                }
            ]
        }
    ])],
    exports: [RouterModule],
    providers: [],
    bootstrap: []
})
export class HomeRoutingModule { }
