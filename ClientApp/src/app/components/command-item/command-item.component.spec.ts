import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandItemComponent } from './command-item.component';

describe('CommandItemComponent', () => {
  let component: CommandItemComponent;
  let fixture: ComponentFixture<CommandItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CommandItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CommandItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
