import { TestBed } from '@angular/core/testing';

import { DeviceConfigurationService } from './device-configuration.service';

describe('DeviceConfigurationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeviceConfigurationService = TestBed.get(DeviceConfigurationService);
    expect(service).toBeTruthy();
  });
});
