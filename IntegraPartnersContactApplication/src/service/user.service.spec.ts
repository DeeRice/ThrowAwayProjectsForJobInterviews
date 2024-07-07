import { TestBed } from '@angular/core/testing';
import { Observable } from 'rxjs/internal/Observable';
import { UserService } from './user.service';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/user';

describe('UserService', () => {
  let service: UserService;
  let httpClient:HttpClient;
  let userid:number = 1;
  let user:User = new User();
  beforeEach(() => {
    TestBed.configureTestingModule({});
    httpClient = TestBed.inject(HttpClient);
    service = TestBed.inject(UserService);
    service = new UserService(httpClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#getUserByID should return value or undefined', (done: DoneFn) => {
    service?.getUserByID(userid)?.subscribe((value) => {
       expect (typeof(value) == typeof(Observable<any>) || typeof(value) == undefined).toBe(true);
      done();
    });
  });

  it('#getUserAllUsers should return value or undefined', (done: DoneFn) => {
    service?.getAllUsers()?.subscribe((value) => {
       expect (typeof(value) == typeof(Observable<any>) || typeof(value) == undefined).toBe(true);
      done();
    });
  });

  it('#addUser should return value or undefined', (done: DoneFn) => {
    service?.addUser(user)?.subscribe((value) => {
       expect (typeof(value) == typeof(Observable<any>) || typeof(value) == undefined).toBe(true);
      done();
    });
  });

  it('#editUser should return value or undefined', (done: DoneFn) => {
    service?.editUser(user)?.subscribe((value) => {
       expect (typeof(value) == typeof(Observable<any>) || typeof(value) == undefined).toBe(true);
      done();
    });
  });

  it('#deleteUser should return value or undefined', (done: DoneFn) => {
    service?.deleteUser(userid)?.subscribe((value) => {
       expect (typeof(value) == typeof(Observable<any>) || typeof(value) == undefined).toBe(true);
      done();
    });
  });


});
