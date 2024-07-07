import { Component, Inject } from '@angular/core';
import { User } from '../../model/user';
import { UserService } from '../../service/user.service';
import { DxDataGridModule } from 'devextreme-angular';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [DxDataGridModule],
  providers: [UserService, DxDataGridModule],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {
  users: User[] = [];
  _userService: UserService;
   constructor(@Inject(ActivatedRoute) activatedRoute: ActivatedRoute, @Inject(Router) router: Router,private userService: UserService){
    this._userService = userService;
    this._userService.getAllUsers()?.subscribe((data) => {
      this.users = data;
  });
}

}
