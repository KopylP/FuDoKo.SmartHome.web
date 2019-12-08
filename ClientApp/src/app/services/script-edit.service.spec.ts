import { TestBed } from '@angular/core/testing';

import { ScriptEditService } from './script-edit.service';

describe('ScriptEditService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ScriptEditService = TestBed.get(ScriptEditService);
    expect(service).toBeTruthy();
  });
});
