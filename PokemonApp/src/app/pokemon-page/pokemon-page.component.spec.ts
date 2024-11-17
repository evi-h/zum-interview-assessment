import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PokemonPageComponent } from './pokemon-page.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { By } from '@angular/platform-browser';

describe('PokemonPageComponent', () => {
  let component: PokemonPageComponent;
  let fixture: ComponentFixture<PokemonPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PokemonPageComponent],
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

  it('should update sortType and sortDirection on dropdown selection change', () => {
    const sortTypeSelect = fixture.debugElement.query(
      By.css('.select-type')
    ).nativeElement;
    const sortDirectionSelect = fixture.debugElement.query(
      By.css('.select-direction')
    ).nativeElement;

    sortTypeSelect.value = 'name';
    sortTypeSelect.dispatchEvent(new Event('change')); // Trigger change event
    fixture.detectChanges();

    sortDirectionSelect.value = 'desc';
    sortDirectionSelect.dispatchEvent(new Event('change')); // Trigger change event
    fixture.detectChanges();

    expect(component.sortType).toBe('name');
    expect(component.sortDirection).toBe('desc');
  });
});
