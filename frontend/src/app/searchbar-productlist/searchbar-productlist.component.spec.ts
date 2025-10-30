import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchbarProductlistComponent } from './searchbar-productlist.component';

describe('SearchbarProductlistComponent', () => {
  let component: SearchbarProductlistComponent;
  let fixture: ComponentFixture<SearchbarProductlistComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchbarProductlistComponent]
    });
    fixture = TestBed.createComponent(SearchbarProductlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
