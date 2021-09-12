# Changelog

## 2.0.1
- Added [JsonConstructor] attribute to CmdIssue for Json.Net deserialization

## 2.0.0
- **Breaking change:** ICmdResult has merged Errors and Warnings collections into Issues with an IssueType enum.
- **Breaking change:** ICmdResult has changed IsSuccessful() to Success property for serialization
- Command handler interfaces now also support custom return type where the return type is based on ICmdResult
- Extension method HandlerPipelineEquals() added to express unit test expectation for decorator pipeline in a more succinct manner.
- Handler registrar updated to allow multiple handler contracts on a single concrete class

## 1.1.1
- Bug fix for handler registrar not supporting the new query handler interface type

## 1.1.0
- Query handler interfaces which don't require an input parameter