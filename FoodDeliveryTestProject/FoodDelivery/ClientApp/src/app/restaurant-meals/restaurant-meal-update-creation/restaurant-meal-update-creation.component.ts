import { ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../alert-system/alert.service';
import { MealImage } from '../../models/meal-image';
import { Meal } from '../../models/meal.model';
import { AuthenticationService } from '../../services/authentication.service';
import { MealsService } from '../../services/meals.service';

@Component({
  selector: 'app-restaurant-meal-update-creation',
  templateUrl: './restaurant-meal-update-creation.component.html'
})
export class RestaurantMealUpdateCreationComponent implements OnInit {

  get m() { return this.mealForm.controls; }

  mealForm = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: ['', [Validators.required, Validators.min(0), Validators.max(10000)]],
    image: ['', Validators.required]
  });

  options = {
    autoClose: true,
    keepAfterRouteChange: true
  };

  public meal: Meal;
  public submitted = false;
  private imageStr = 'data:image/jpeg;base64,';

  constructor(private route: ActivatedRoute,
    private router: Router,
    private mealsService: MealsService,
    private formBuilder: FormBuilder,
    private cd: ChangeDetectorRef,
    private alertService: AlertService,
    private authenticationService: AuthenticationService) {
  }
  async ngOnInit(): Promise<void> {
    this.route.params.subscribe(async (params) => {
      if (params) {
        const mealId = params['mid'];
        const restaurantId = params['rid'];
        if (mealId) {
          this.meal = await this.mealsService.getMeal(mealId);
          this.m.name.setValue(this.meal.name);
          this.m.price.setValue(this.meal.price);
          this.m.description.setValue(this.meal.description);
          var image = await this.mealsService.getImage(mealId);
          this.m.image.setValue(this.formatImage(image));
          return;
        }
        else {
          this.meal = <Meal>{};
          this.meal.restaurantId = restaurantId;
        }
      }
    });
  }
  onFileChange(event) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        if (!reader.result.toString().startsWith(this.imageStr)) {
          this.alertService.warn("Please use jpeg file", this.options);
          return;
        }
        this.mealForm.patchValue({
          image: reader.result
        });
        this.cd.markForCheck();
      };
    }
  }

  async onSubmit(): Promise<void> {
    this.submitted = true;
    if (this.mealForm.invalid) {
      return;
    }
    this.meal.description = this.m.description.value;
    this.meal.name = this.m.name.value;
    this.meal.price = this.m.price.value;
    this.meal.image = <MealImage>{};
    this.meal.image.image = this.formatArrayFromImage(this.m.image.value);
    if (!this.meal.entityId) {
      await this.mealsService.addMeal(this.meal, this.authenticationService.getUserId);
      this.alertService.success("New meal added", this.options);
    }
    else {
      await this.mealsService.updateMeal(this.meal, this.authenticationService.getUserId);
      this.alertService.success("Meal updated", this.options);
    }
    this.router.navigate(['/restaurant/' + this.meal.restaurantId]);
  }

  formatImage(img: any): any {
    return 'data:image/jpeg;base64,' + img;
  }

  formatArrayFromImage(img: string): any {
    return img.replace(this.imageStr, '');
  }
}
