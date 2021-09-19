import React from "react";
import { FC } from "react";
import CardTextField from "./CardFormTextField";
import * as Constants from "../../../Constants";
import { InfoCardData } from "../../CardList/InfoCard/InfoCard";

export interface CardFormInput {
    cardData: InfoCardData;
    titleError: boolean;
    descriptionError: boolean;
}

interface Props {
    input: CardFormInput;
    handleChange: React.Dispatch<
        React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    >;
}

const CardForm: FC<Props> = ({ input, handleChange }) => {
    return (
        <>
            <CardTextField
                autoFocus
                label="Title"
                name="title"
                value={input.cardData.title}
                onChange={handleChange}
                required
                error={input.titleError}
                helperText={
                    input.titleError ? Constants.REQUIRED_FIELD_MSG : ""
                }
            />
            <CardTextField
                label="Description"
                name="description"
                value={input.cardData.description}
                onChange={handleChange}
                error={input.descriptionError}
                helperText={
                    input.descriptionError ? Constants.REQUIRED_FIELD_MSG : ""
                }
                multiline
                required
            />
            <CardTextField
                label="Image (URL)"
                type="url"
                name="image"
                value={input.cardData.image}
                onChange={handleChange}
            />
            <input type="hidden" name="key" value={input.cardData.key} />
        </>
    );
};

export default CardForm;
