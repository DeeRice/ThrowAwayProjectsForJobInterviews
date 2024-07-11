import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, lastValueFrom } from 'rxjs';
import applyChanges from 'devextreme/data/apply_changes';
import { User } from '../model/user';
import { Change } from '../model/change';
import { ApiResponse} from '../model/apiresponse';
import { UserViewModel } from '../model/user-view-model';

@Injectable({
  providedIn: 'root'
})
export class UserExpressGridService {
  public currentUserViewModel!: UserViewModel;
  private users$ = new BehaviorSubject<User[]>([]);
  public baseUrl ="https://localhost:44335"
  private url = 'https://js.devexpress.com/Demos/Mvc/api/DataGridWebApi';
  public _httpClient!: HttpClient;
  constructor(private httpClient: HttpClient) { 
    this._httpClient = httpClient;
  }

  updateUsers(change: Change<User>, data: User) {
    change.data = data;
    const users = applyChanges(this.users$.getValue(), [change], { keyExpr: 'userID' });
    this.users$.next(users);
  }

  getAllUsers(): Observable<User[]> {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8')
    .set('Accept', 'application/json');
    lastValueFrom(this._httpClient.get(`${this.baseUrl}/Users/GetAllUsers`, {headers: headers}))
      .then((data: any) => {
        return this.users$.next(data);
      });
    return this.users$.asObservable();
  }

  async insert(change: Change<User>): Promise<User> {
    var userViewModel = this.mapDataCreate(change.data);
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json; charset=utf-8')
    .set('Accept', 'application/json');
    const data = await lastValueFrom(this._httpClient.post<User>(`${this.baseUrl}/Users/CreateUser`, JSON.stringify(userViewModel), {headers: headers})); 
    return data;
  }

  async update(change: Change<User>): Promise<User> {
    debugger;
    this.currentUserViewModel = this.mapDataEdit(change.data);
    const httpOptions = {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    const data = await lastValueFrom(this._httpClient.put<User>(`${this.baseUrl}/Users/EditUser/${this.currentUserViewModel.UserID}`, JSON.stringify(this.currentUserViewModel), httpOptions));

    this.updateUsers(change, data);

    return data;
  }

  async remove(change: Change<User>): Promise<User> {
    var UserID = change.key;
    let headers = new HttpHeaders();
     headers.set('Content-Type', 'application/json; charset=utf-8')
    .set('Accept', 'application/json');
    const data = await lastValueFrom(this._httpClient.delete<User>(`${this.baseUrl}/Users/DeleteUser/${UserID}`, {headers: headers}));
    this.updateUsers(change, data);
    return data;
  }

  async saveChange(change: Change<User>): Promise<User> {
    switch (change.type) {
      case 'insert':
        return this.insert(change);
      case 'update':
        return this.update(change);
      case 'remove':
        return this.remove(change);
    }
  }
  mapDataCreate(user: Partial<User>){
    var viewModel: UserViewModel = new UserViewModel();
    viewModel.UserID = user?.userID ?? -1;
    viewModel.Username = user?.username ?? "";
    viewModel.FirstName = user?.firstName ?? "";
    viewModel.LastName = user?.lastName ?? "";
    viewModel.UserStatus = user?.userStatus ?? "";
    viewModel.Email = user?.email ?? "";
    viewModel.Department =  user?.department ?? "";
    return viewModel;
  }
  setViewModel(vm:User): UserViewModel{
    debugger;
   this.currentUserViewModel = new UserViewModel();
   this.currentUserViewModel.Department = vm.department;
   this.currentUserViewModel.Email = vm.email;
   this.currentUserViewModel.FirstName = vm.firstName;
   this.currentUserViewModel.LastName = vm.lastName;
   this.currentUserViewModel.UserID = vm.userID;
   this.currentUserViewModel.UserStatus = vm.userStatus;
   this.currentUserViewModel.Username = vm.username;
   return this.currentUserViewModel;
  }

  mapDataEdit(user: Partial<User>): UserViewModel {
   if('department' in user){
    this.currentUserViewModel.Department = user.department?.valueOf() || "";
   }

   if('email' in user){
    this.currentUserViewModel.Email = user.email?.valueOf() || "";
   }

   if('firstName' in user){
    this.currentUserViewModel.FirstName = user.firstName?.valueOf() || "";
  }

  if('lastName' in user){
    this.currentUserViewModel.LastName = user.lastName?.valueOf() || "";
  }

  if('userID' in user){
    this.currentUserViewModel.UserID = user.userID?.valueOf() || 0;
  }

  if('userStatus' in user){
    this.currentUserViewModel.UserStatus = user.userStatus?.valueOf() || "";
  }

  if('username' in user){
    this.currentUserViewModel.Username = user.username?.valueOf() || "";
  }
  
    return this.currentUserViewModel;
  
 }
}
