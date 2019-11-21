import { TestBed } from '@angular/core/testing';

import { AccessControllerService } from './access-controller.service';

describe('AccessControllerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AccessControllerService = TestBed.get(AccessControllerService);
    expect(service).toBeTruthy();
  });
});
