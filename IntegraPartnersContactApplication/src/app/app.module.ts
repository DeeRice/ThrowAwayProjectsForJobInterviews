import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
 
import { DxDataGridModule } from 'devextreme-angular';
import { HttpClientModule, provideHttpClient, withJsonpSupport } from '@angular/common/http';
import { routes } from './app.routes';
import { RouterModule, provideRouter, withComponentInputBinding } from '@angular/router';
import { UsersComponent } from './users/users.component';
 
@NgModule({
    imports: [
        BrowserModule,
        DxDataGridModule,RouterModule, RouterModule.forRoot([
            {path: 'app-users', component: UsersComponent},
        ])
    ],
    providers: [provideRouter(routes, withComponentInputBinding()), provideHttpClient(withJsonpSupport()),
        HttpClientModule
    ]
})
export class AppModule { }

bootstrapApplication(AppComponent, {
    providers: [provideHttpClient(withJsonpSupport())]
  });

