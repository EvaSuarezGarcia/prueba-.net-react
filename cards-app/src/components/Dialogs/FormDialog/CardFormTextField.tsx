import { TextField, TextFieldProps } from "@mui/material";
import { FC } from "react";

const CardTextField: FC<TextFieldProps> = (props) => {
    return (
        <TextField
            margin="dense"
            type="text"
            fullWidth
            variant="standard"
            {...props}
        />
    );
};

export default CardTextField;
