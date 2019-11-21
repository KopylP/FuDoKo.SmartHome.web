import { TestBed } from '@angular/core/testing';

import { CotrollerAccessService } from './cotroller-access.service';

describe('CotrollerAccessService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CotrollerAccessService = TestBed.get(CotrollerAccessService);
    expect(service).toBeTruthy();
  });
});
