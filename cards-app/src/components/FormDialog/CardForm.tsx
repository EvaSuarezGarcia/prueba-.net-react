import React from "react";
import { FC } from "react";
import { InputState as InputProps } from "./CardFormDialog";
import CardTextField from "./CardFormTextField";
import * as Constants from "../../Constants";

interface Props {
    input: InputProps;
    handleChange: React.Dispatch<
        React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    >;
}

const CardForm: FC<Props> = ({ input, handleChange }) => {
    return (
        <>
            <CardTextField
                autoFocus
                id="title-input"
                label="Title"
                name="title"
                value={input.title}
                onChange={handleChange}
                required
                error={input.titleError}
                helperText={
                    input.titleError ? Constants.REQUIRED_FIELD_MSG : ""
                }
            />
            <CardTextField
                id="description-input"
                label="Description"
                name="description"
                value={input.description}
                onChange={handleChange}
                error={input.descriptionError}
                helperText={
                    input.descriptionError ? Constants.REQUIRED_FIELD_MSG : ""
                }
                multiline
                required
            />
            <CardTextField
                id="image-input"
                label="Image (URL)"
                type="url"
                name="image"
                value={input.image}
                onChange={handleChange}
            />
        </>
    );
};

export default CardForm;
