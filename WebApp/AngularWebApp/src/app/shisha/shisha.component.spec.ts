import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShishaComponent } from './shisha.component';

describe('ShishaComponent', () => {
  let component: ShishaComponent;
  let fixture: ComponentFixture<ShishaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShishaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShishaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
