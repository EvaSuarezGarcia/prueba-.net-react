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

    const createCard = (cardTitle, cardDescription, cardImg) => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type(cardTitle);
        cy.get("textarea[name='description']").type(cardDescription);
        if (cardImg) {
            cy.get("[name='image']").type(cardImg);
        }
        cy.contains(Constants.ADD).click();
    }

    it("can create a card without image", () => {
        createCard(title, description);
        cy.contains(title).parent("div").siblings("img").should("have.attr", "src").should("include", Constants.DEFAULT_IMAGE_URL);
    });

    it("can create a card with image", () => {
        createCard(title, description, img);
        cy.contains(title).parent("div").siblings("img").should("have.attr", "src").should("include", img);
    });

    describe("when a card has been created", () => {
        beforeEach(() => {
            createCard(title, description);
            cy.get(".card-actions").invoke("show");
        });

        it("add modal has empty inputs", () => {
            cy.get("[aria-label='add']").click();
            cy.get("[name='title']").should("have.value", "");
            cy.get("textarea[name='description']").should("have.value", "");
            cy.get("[name='image']").should("have.value", "");
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

            // Edit modal shows updated values
            cy.get("[aria-label='edit']").click();
            cy.get("[name='title']").should("have.value", titleEdit);
            cy.get("textarea[name='description']").should("have.value", descriptionEdit);
            cy.get("[name='image']").should("have.value", img);
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
                createCard(title2, description2);
                createCard(title3, description3);
            });

            const checkCardsOrder = (expectedTitles) => {
                cy.get(".card h5").each((title, index) => {
                    cy.wrap(title).should("have.text", expectedTitles[index]);
                });
            }

            it("can sort by title", () => {
                // Asc
                cy.get("button").contains(Constants.SORT_BY_TITLE).click();
                checkCardsOrder([title3, title, title2]);

                // Desc
                cy.get("button").contains(Constants.SORT_BY_TITLE).click();
                checkCardsOrder([title2, title, title3]);
            });

            it("can sort by creation date", () => {
                // Desc
                cy.get("button").contains(Constants.SORT_BY_CREATION_DATE).click();
                checkCardsOrder([title3, title2, title]);

                // Asc
                cy.get("button").contains(Constants.SORT_BY_CREATION_DATE).click();
                checkCardsOrder([title, title2, title3]);
            });
        });
    });
});