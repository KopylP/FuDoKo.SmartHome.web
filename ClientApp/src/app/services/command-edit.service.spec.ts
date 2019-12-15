import { TestBed } from '@angular/core/testing';

import { CommandEditService } from './command-edit.service';

describe('CommandEditService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CommandEditService = TestBed.get(CommandEditService);
    expect(service).toBeTruthy();
  });
});
