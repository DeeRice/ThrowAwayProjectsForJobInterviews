import { TestBed } from '@angular/core/testing';

import { UserExpressGridService } from './user-express-grid.service';

describe('UserExpressGridService', () => {
  let service: UserExpressGridService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserExpressGridService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
