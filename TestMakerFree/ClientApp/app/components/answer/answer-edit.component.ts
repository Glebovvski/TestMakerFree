﻿import { Component, Inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";

@Component({
    selector: 'answer-edit',
    templateUrl: './answer-edit.component.html',
    styleUrls: ['./answer-edit.component']
})

export class AnswerEditComponent {
    title: string;
    answer: Answer;

    editMode: boolean;

    constructor(private activatedRoute: ActivatedRoute,
        private router: Router,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {
        this.answer = <Answer>{};

        var id = +this.activatedRoute.snapshot.params["id"];

        this.editMode = (this.activatedRoute.snapshot.url[1].path === "edit");
        if (this.editMode) {
            var url = this.baseUrl + "api/answer/" + id;
            this.http.get<Answer>(url).subscribe(res => {
                this.answer = res;
                this.title = "Edit - " + this.answer.Text;
            }, error => console.error(error));
        }
        else {
            this.answer.QuestionId = id;
            this.title = "Create a new answer";
        }
    }

    onSubmit(answer: Answer) {
        var url = this.baseUrl + "api/answer";

        if (this.editMode) {
            this.http.put<Answer>(url, answer).subscribe(res => {
                var v = res;
                console.log("answer " + v.Id + " has been updated");
                this.router.navigate(["answer/edit", v.QuestionId]);
            }, error => console.error(error));
        }
        else {
            this.http.post<Answer>(url, answer).subscribe(res => {
                var v = res;
                console.log("Answer " + v.Id + " has been created");
                this.router.navigate(["answer/edit", v.QuestionId]);
            }, error => console.error(error));
        }
    }

    onBack() {
        this.router.navigate(["answer/edit", this.answer.QuestionId]);
    }
}