import { TestBed } from '@angular/core/testing';

import { DeviceAccessService } from './device-access.service';

describe('DeviceAccessService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeviceAccessService = TestBed.get(DeviceAccessService);
    expect(service).toBeTruthy();
  });
});
