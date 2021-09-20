import * as Constants from "../../src/Constants";

describe("Card App", () => {
    const title = "Gato";
    const description = "Descripción gato";
    const img = "https://ichef.bbci.co.uk/news/640/cpsprodpb/10E9B/production/_109757296_gettyimages-1128004359.jpg";

    beforeEach(() => {
        cy.visit("http://localhost:3000");
    });

    it("frontpage can be opened", () => {
        cy.get("[aria-label='add']");
    });

    it("can create a card without image", () => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type(title);
        cy.get("textarea[name='description']").type(description);
        cy.contains(Constants.ADD).click();
        cy.contains(title).parent("div").siblings("img").should("have.attr", "src").should("include", "gato-marron");
    });

    it("can create a card with image", () => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type(title);
        cy.get("textarea[name='description']").type(description);
        cy.get("[name='image']").type(img);
        cy.contains(Constants.ADD).click();
        cy.contains(title).parent("div").siblings("img").should("have.attr", "src").should("include", img);
    });

    describe("when a card exists", () => {
        beforeEach(() => {
            cy.get("[aria-label='add']").click();
            cy.get("[name='title']").type(title);
            cy.get("textarea[name='description']").type(description);
            cy.contains(Constants.ADD).click();
            cy.get(".card-actions").invoke("show");
        });

        it("a card has actions", () => {
            cy.get("[aria-label='edit']");
            cy.get("[aria-label='delete']");
        });

        it("can edit card", () => {
            const titleEdit = "Test Edit";
            const descriptionEdit = "Description test edit";

            cy.get("[aria-label='edit']").click();
            cy.get("[name='title']").clear().type(titleEdit);
            cy.get("textarea[name='description']").clear().type(descriptionEdit);
            cy.get("[name='image']").clear().type(img);
            cy.get("button").contains(Constants.EDIT).click();
            cy.contains(titleEdit);
            cy.contains(descriptionEdit);
            cy.contains(titleEdit).parent("div").siblings("img").should("have.attr", "src").should("include", img);
        });

        it("can delete card", () => {
            cy.get("[aria-label='delete']").click();
            cy.get("button").contains(Constants.DELETE).click();
            cy.contains(title).should("not.exist");
        });

        it("card is saved", () => {
            cy.reload();
            cy.contains(title);
        });

        describe("when several cards exist", () => {
            const title2 = "Perro";
            const description2 = "Descripción perro";
            const title3 = "Cisne";
            const description3 = "Descripción cisne";

            beforeEach(() => {
                cy.get("[aria-label='add']").click();
                cy.get("[name='title']").type(title2);
                cy.get("textarea[name='description']").type(description2);
                cy.contains(Constants.ADD).click();

                cy.get("[aria-label='add']").click();
                cy.get("[name='title']").type(title3);
                cy.get("textarea[name='description']").type(description3);
                cy.contains(Constants.ADD).click();
            });

            it("can sort by title", () => {
                // Asc
                cy.get("button").contains(Constants.SORT_BY_TITLE).click();
                cy.get(".card").eq(0).get("h5").contains(title3);
                cy.get(".card").eq(1).get("h5").contains(title);
                cy.get(".card").eq(2).get("h5").contains(title2);

                // Desc
                cy.get("button").contains(Constants.SORT_BY_TITLE).click();
                cy.get(".card").eq(0).get("h5").contains(title2);
                cy.get(".card").eq(1).get("h5").contains(title);
                cy.get(".card").eq(2).get("h5").contains(title3);
            });

            it("can sort by creation date", () => {
                // Desc
                cy.get("button").contains(Constants.SORT_BY_CREATION_DATE).click();
                cy.get(".card").eq(0).get("h5").contains(title3);
                cy.get(".card").eq(1).get("h5").contains(title2);
                cy.get(".card").eq(2).get("h5").contains(title);

                // Asc
                cy.get("button").contains(Constants.SORT_BY_CREATION_DATE).click();
                cy.get(".card").eq(0).get("h5").contains(title);
                cy.get(".card").eq(1).get("h5").contains(title2);
                cy.get(".card").eq(2).get("h5").contains(title3);
            });
        });
    });
});