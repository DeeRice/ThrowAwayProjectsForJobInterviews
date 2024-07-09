import { Component, Inject } from '@angular/core';
import { User } from '../../model/user';
import { UserService } from '../../service/user.service';
import { DxDataGridModule, DxLoadPanelModule } from 'devextreme-angular';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { Subscription } from 'rxjs/internal/Subscription';
import { ApiResponse } from '../../model/apiresponse';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { Change } from '../../model/change';
import { UserExpressGridService } from './../../service/user-express-grid.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [DxDataGridModule,  DxLoadPanelModule, HttpClientModule],
  providers: [UserService, DxDataGridModule,  DxLoadPanelModule,
    HttpClientModule],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {
  usersSubscription!: Subscription;

  users$!: Observable<User[]>;

  changes: Change<User>[] = [];

  editRowKey: number = -1;

  isLoading = false;

  loadPanelPosition = { of: '#gridContainer' };
  users: User[] = [];
  _userService: UserService;
  _userExpressGridService: UserExpressGridService;
   constructor(@Inject(ActivatedRoute) activatedRoute: ActivatedRoute, @Inject(Router) router: Router,private userService: UserService,
   userExpressGridService: UserExpressGridService){
    this._userService = userService;
    this._userExpressGridService = userExpressGridService;
    this._userService.getAllUsers()?.subscribe((data) => {
      this.users = data;
  });
}
      ngOnInit() {
        this.users$ = this._userExpressGridService.getAllUsers();
      
        this.isLoading = true;
        this.usersSubscription = this.users$.subscribe(() => {
          this.isLoading = false;
        });
     }

     get changesText(): string {
      return JSON.stringify(this.changes.map((change) => ({
        type: change.type,
        key: change.type !== 'insert' ? change.key : undefined,
        data: change.data,
      })), null, ' ');
    }
  
    onSaving(e: DxDataGridTypes.SavingEvent) {
      const change = e.changes[0];
  
      if (change) {
        e.cancel = true;
        e.promise = this.processSaving(change);
      }
    }
  
    async processSaving(change: Change<User>) {
      this.isLoading = true;
  
      try {
        await this._userExpressGridService.saveChange(change);
        this.editRowKey = -1;
        this.changes = [];
      } finally {
        this.isLoading = false;
      }
    }
  
    ngOnDestroy() {
      this.usersSubscription.unsubscribe();
  }
}


