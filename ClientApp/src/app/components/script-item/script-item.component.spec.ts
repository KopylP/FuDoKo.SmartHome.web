import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScriptItemComponent } from './script-item.component';

describe('ScriptItemComponent', () => {
  let component: ScriptItemComponent;
  let fixture: ComponentFixture<ScriptItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScriptItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScriptItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
