import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuybackComponent } from './buyback.component';

describe('BuybackComponent', () => {
  let component: BuybackComponent;
  let fixture: ComponentFixture<BuybackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuybackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuybackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
