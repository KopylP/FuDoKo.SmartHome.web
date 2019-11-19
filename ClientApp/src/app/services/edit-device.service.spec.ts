import { TestBed } from '@angular/core/testing';

import { EditDeviceService } from './edit-device.service';

describe('EditDeviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EditDeviceService = TestBed.get(EditDeviceService);
    expect(service).toBeTruthy();
  });
});
