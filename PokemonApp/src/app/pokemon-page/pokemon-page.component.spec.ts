import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { By } from '@angular/platform-browser';

import { PokemonPageComponent } from './pokemon-page.component';
import { SharedSelectComponent } from '../shared-select/shared-select.component';

describe('PokemonPageComponent', () => {
  let component: PokemonPageComponent;
  let fixture: ComponentFixture<PokemonPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PokemonPageComponent, SharedSelectComponent],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();

    fixture = TestBed.createComponent(PokemonPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call sort() method when search button is clicked', () => {
    spyOn(component, 'sort'); // Spy on the sort method

    const button = fixture.debugElement.query(By.css('button')).nativeElement;
    button.click(); // Simulate the button click

    expect(component.sort).toHaveBeenCalled(); // Ensure the sort method was called
  });

  it('should update sortType on dropdown selection change', () => {
    const sortTypeSelectDebug = fixture.debugElement.query(
      By.css('.select-type')
    );

    const sharedSelectComponent =
      sortTypeSelectDebug.componentInstance as SharedSelectComponent;

    expect(component.sortType).toBe('id');

    // Simulate selection change in SharedSelectComponent
    sharedSelectComponent.selectedOptionChange.emit('name');
    fixture.detectChanges();

    // Assert updated value of sortType in parent
    expect(component.sortType).toBe('name');
  });

  it('should update sortDirection on dropdown selection change', () => {
    const sortDirectionSelectDebug = fixture.debugElement.query(
      By.css('.select-direction')
    );

    const sharedSelectComponent =
      sortDirectionSelectDebug.componentInstance as SharedSelectComponent;

    expect(component.sortDirection).toBe('desc');

    // Simulate selection change in SharedSelectComponent
    sharedSelectComponent.selectedOptionChange.emit('asc');
    fixture.detectChanges();

    // Assert updated value of sortDirection in parent
    expect(component.sortDirection).toBe('asc');
  });
});
