import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleNewsItemComponent } from './single-news-item.component';

describe('SingleNewsItemComponent', () => {
  let component: SingleNewsItemComponent;
  let fixture: ComponentFixture<SingleNewsItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SingleNewsItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SingleNewsItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
