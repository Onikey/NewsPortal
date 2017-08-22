import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

@NgModule({
    declarations: [],
    imports: [
        RouterModule.forRoot([
            { path: 'admin', loadChildren: './admin/admin.module#AdminModule' },
            { path: '', loadChildren: './home/home.module#HomeModule' }
        ])
    ],
    exports: [RouterModule],
    providers: [],
    bootstrap: []
})
export class AppRoutingModule { }
