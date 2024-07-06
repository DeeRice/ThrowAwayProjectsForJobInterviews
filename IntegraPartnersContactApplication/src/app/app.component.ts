import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { UsersComponent } from './users/users.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UsersComponent, RouterModule],
  providers: [RouterOutlet, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'IntegraPartnersContactApplication';
}
