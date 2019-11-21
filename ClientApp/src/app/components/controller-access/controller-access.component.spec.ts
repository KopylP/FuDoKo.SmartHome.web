import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerAccessComponent } from './controller-access.component';

describe('ControllerAccessComponent', () => {
  let component: ControllerAccessComponent;
  let fixture: ComponentFixture<ControllerAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerAccessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
