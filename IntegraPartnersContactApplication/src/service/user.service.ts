import { Injectable } from '@angular/core';
import { User } from '../model/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  public baseUrl ="https://localhost:44335/"
  public getUserByIDUrl: string = this.baseUrl + "/GetUser/";
  public getAllUsersUrl: string = this.baseUrl + "/GetAllUsers";
  public addUserUrl: string = this.baseUrl + "/CreateUser";
  public updateUserUrl: string = this.baseUrl + "/EditUser";
  public deleteUserUrl: string = this.baseUrl + "/DeleteUser/";
  public _httpClient?: HttpClient;
  public _currentUserID: number = -1;
    constructor(private httpClient: HttpClient) { 
      this._httpClient = httpClient;
    }
  
    getUserByID(userID: number) : Observable<any> | undefined {
      let params = new HttpParams().set('UserID', userID);
      return this._httpClient?.get(this.getUserByIDUrl, { params: params });
    }
  
    getAllUsers() : Observable<any> | undefined {
      return this._httpClient?.get(this.getAllUsersUrl);
    }
  
    addUser(user: User) : Observable<any> | undefined {
      let params = new HttpParams().set('User', JSON.stringify(user));
      return this._httpClient?.post(this.addUserUrl, { params: params });
    }
  
    editUser(user: User) : Observable<any> | undefined {
      let params = new HttpParams().set("UserID", user.user_id.toString())
      .set('User', JSON.stringify(user));
      return this._httpClient?.put(this.updateUserUrl, { params: params });
    }
    
    deleteUser(userID: number) : Observable<any> | undefined {
      let params = new HttpParams().set('UserID', userID);
      return this._httpClient?.delete(this.deleteUserUrl, { params: params });
    }
  
}
