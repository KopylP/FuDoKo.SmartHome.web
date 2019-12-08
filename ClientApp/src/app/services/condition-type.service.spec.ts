import { TestBed } from '@angular/core/testing';

import { ConditionTypeService } from './condition-type.service';

describe('ConditionTypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConditionTypeService = TestBed.get(ConditionTypeService);
    expect(service).toBeTruthy();
  });
});
