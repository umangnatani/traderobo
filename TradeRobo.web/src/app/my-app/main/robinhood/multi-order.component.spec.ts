import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MultiOrderComponent } from './multi-order.component';

describe('MultiOrderComponent', () => {
  let component: MultiOrderComponent;
  let fixture: ComponentFixture<MultiOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MultiOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MultiOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
