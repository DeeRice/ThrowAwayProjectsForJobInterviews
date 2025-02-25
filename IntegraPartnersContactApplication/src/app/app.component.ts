import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { UsersComponent } from './users/users.component';
import { HttpClientModule } from '@angular/common/http';
import { DxDataGridModule, DxLoadPanelModule } from 'devextreme-angular';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UsersComponent, RouterModule, HttpClientModule, DxLoadPanelModule, HttpClientModule,
    DxDataGridModule
  ],
  providers: [RouterOutlet, RouterModule, HttpClientModule, DxLoadPanelModule, DxDataGridModule,
    HttpClientModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'IntegraPartnersContactApplication';
  constructor(){

  }
}
