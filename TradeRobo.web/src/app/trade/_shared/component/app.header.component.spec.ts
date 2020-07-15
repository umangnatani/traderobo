import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { App.HeaderComponent } from './app.header.component';

describe('App.HeaderComponent', () => {
  let component: App.HeaderComponent;
  let fixture: ComponentFixture<App.HeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ App.HeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(App.HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
