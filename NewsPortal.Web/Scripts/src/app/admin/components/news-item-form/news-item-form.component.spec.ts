import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsItemFormComponent } from './news-item-form.component';

describe('NewsItemFormComponent', () => {
  let component: NewsItemFormComponent;
  let fixture: ComponentFixture<NewsItemFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewsItemFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsItemFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
