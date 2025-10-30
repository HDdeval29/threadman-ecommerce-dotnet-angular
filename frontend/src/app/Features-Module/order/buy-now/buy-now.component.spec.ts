import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyNowComponent } from './buy-now.component';

describe('BuyNowComponent', () => {
  let component: BuyNowComponent;
  let fixture: ComponentFixture<BuyNowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BuyNowComponent]
    });
    fixture = TestBed.createComponent(BuyNowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
