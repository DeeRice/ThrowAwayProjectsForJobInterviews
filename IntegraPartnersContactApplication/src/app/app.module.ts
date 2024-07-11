import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { DxDataGridModule, DxLoadPanelModule } from 'devextreme-angular';
import { HttpClientModule, provideHttpClient, withJsonpSupport } from '@angular/common/http';
import { routes } from './app.routes';
import { RouterModule, provideRouter, withComponentInputBinding } from '@angular/router';
import { UsersComponent } from './users/users.component';
 
@NgModule({
    imports: [
        BrowserModule,
        DxDataGridModule,RouterModule, DxLoadPanelModule, RouterModule.forRoot([
            {path: 'app-users', component: UsersComponent},
        ]), HttpClientModule
    ],
    providers: [provideRouter(routes, withComponentInputBinding()), provideHttpClient(withJsonpSupport()),
        HttpClientModule, DxLoadPanelModule, DxDataGridModule
    ]
})
export class AppModule { }


platformBrowserDynamic().bootstrapModule(AppModule);
bootstrapApplication(AppComponent, {
    providers: [provideHttpClient(withJsonpSupport())]
  });

