import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, lastValueFrom } from 'rxjs';
import applyChanges from 'devextreme/data/apply_changes';
import { User } from '../model/user';
import { Change } from '../model/change';
import { ApiResponse} from '../model/apiresponse';

@Injectable({
  providedIn: 'root'
})
export class UserExpressGridService {

  private users$ = new BehaviorSubject<User[]>([]);
  public baseUrl ="https://localhost:44335"
  private url = 'https://js.devexpress.com/Demos/Mvc/api/DataGridWebApi';
  public _httpClient!: HttpClient;
  constructor(private httpClient: HttpClient) { 
    this._httpClient = httpClient;
  }

  updateUsers(change: Change<User>, data: User) {
    change.data = data;
    const users = applyChanges(this.users$.getValue(), [change], { keyExpr: 'UserID' });
    this.users$.next(users);
  }

  getAllUsers(): Observable<User[]> {
    lastValueFrom(this._httpClient.get(`${this.baseUrl}/GetAllUsers`))
      .then((data: any) => {
        return this.users$.next(data.data);
      });

    return this.users$.asObservable();
  }

  async insert(change: Change<User>): Promise<User> {
    const httpParams = new HttpParams({ fromObject: { values: JSON.stringify(change.data) } });
    const httpOptions = { body: httpParams };
    const data = await lastValueFrom(this._httpClient.post<User>(`${this.baseUrl}/CreateUser`, httpOptions));

    this.updateUsers(change, data);

    return data;
  }

  async update(change: Change<User>): Promise<User> {
    const httpParams = new HttpParams({ fromObject: { key: change.key, values: JSON.stringify(change.data) } });
    const httpOptions = { body: httpParams };
    const data = await lastValueFrom(this._httpClient.put<User>(`${this.baseUrl}/EditUser`, httpOptions));

    this.updateUsers(change, data);

    return data;
  }

  async remove(change: Change<User>): Promise<User> {
    const httpParams = new HttpParams({ fromObject: { key: change.key } });
    const httpOptions = { body: httpParams };
    const data = await lastValueFrom(this._httpClient.delete<User>(`${this.baseUrl}/DeleteUser`, httpOptions));

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
}
